import { HttpTransportType, HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { selectToken } from "../../features/token/tokenSlice";

export default function Lobby() {
    const token = useAppSelector(selectToken);
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const [connetion, setConnection] = useState<HubConnection>();

    useEffect(() => {
        if (token) {
            const connect = new HubConnectionBuilder()
            .withUrl("http://localhost:5000/hubs/menu", {
                accessTokenFactory: () => token,
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
            })
            .configureLogging(LogLevel.Trace)
            .withAutomaticReconnect()
            .build();

            setConnection(connect);
        }
        else{
            navigate("/login");
        }
    }, []);

    useEffect(() => {
        if (connetion){
            connetion.start()
                .then(result => {
                    console.log("Connection - connected!");
                    connetion.on("ShowOnlineUsers", ShowUsers);
                    connetion.invoke("GetOnline")
                        .then(r => {
                            console.log(r);
                    })
                })
                .catch(err => {
                    console.log("Connection faled:", err);
                });
        }
    }, [connetion]);

    function ShowUsers(r:any) {
        console.log("Show online users:", r);
        console.log(r);
    } 

    const GetOnlineUsers = () => {

    }

    const Disconnect = () => {

    }

    return(
        <>
            <div>Wellcome to Lobby</div>

            <div>
                <button onClick={GetOnlineUsers}>Get Online Users</button>
                <button onClick={Disconnect}>Disconnect</button>
            </div>
        </>
    );
}