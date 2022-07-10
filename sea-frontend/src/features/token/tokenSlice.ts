import { RootState } from './../../app/store';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface TokenState {
    value: string | null;
}

const initialState: TokenState = {
    value: null,
}

export const tokenSlice = createSlice({
    name: "token",
    initialState,
    reducers: {
        setToken: (state, action: PayloadAction<string>) => {
            state.value = action.payload;
        },
        removeToken: state => {
            state.value = null;
        }
    }
});

export const { setToken, removeToken } = tokenSlice.actions;

export const selectToken = (state: RootState) => state.token.value;

export default tokenSlice.reducer;