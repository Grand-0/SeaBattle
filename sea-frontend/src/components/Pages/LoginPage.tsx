import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { selectToken, setToken } from "../../features/token/tokenSlice";

export default function LoginPage() {
    const token = useAppSelector(selectToken);
    const dispatch = useAppDispatch();
    const [login, setLogin] = useState<string>("");
    const [pass, setPass] = useState<string>("");
    const navigate = useNavigate();
  
    const loginHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
      setLogin(e.target.value);
    }
  
    const passHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
      setPass(e.target.value);
    }
  
    const logInHandler = async() => {
      const data = new URLSearchParams();
      data.append("login", login);
      data.append("password", pass);
  
      const response = await fetch('http://localhost:5000/api/auth/login',
        {
          method: "POST",
          mode: "cors",
          cache: "no-cache",
          body: data,
        });
  
      console.log(response.status);
  
      if (response.ok) {
        const result = await response.text();
        console.log(result);
        dispatch(setToken(result));
        navigate("/");
      }
    }

    return (
        <div className="App">
          <span>Login</span>
          <input onChange={loginHandler}/>
          <hr/>
          <span>Password</span>
          <input onChange={passHandler}/>
          <hr/>
          <button onClick={logInHandler}>LogIn</button>
        </div>
      );
}