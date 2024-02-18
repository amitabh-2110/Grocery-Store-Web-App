import { createSlice } from "@reduxjs/toolkit"

const initialState = {
  token: localStorage.getItem("token")? JSON.parse(localStorage.getItem("token")): null,
  role: "",
  fullname: "",
  email: ""
};

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setToken: (state, action) => {
      state.token = action.payload;
    },
    setRole: (state, action) => {
      state.role = action.payload;
    },
    setFullName: (state, action) => {
      state.fullname = action.payload;
    },
    setEmail: (state, action) => {
      state.email = action.payload;
    }
  }
});

export const { setToken, setRole, setFullName, setEmail } = authSlice.actions
export default authSlice.reducer