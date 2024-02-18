import React, { useState } from 'react'
import { toast } from 'react-hot-toast';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { setToken } from '../redux/slices/authSlice';

const baseUrl = "https://localhost:7272/api/UserAuth/login";

const Login = () => {
  const [userLogin, setUserLogin] = useState({
    Email: "",
    Password: ""
  });

  const navigate = useNavigate();
  const dispath = useDispatch();

  const changeHandler = (event) => {
    setUserLogin({
      ...userLogin,
      [event.target.name]: event.target.value
    });
  };

  const submitHandler = async (event) => {
    event.preventDefault();
    let form = new FormData();

    form.append("Email", userLogin.Email);
    form.append("Password", userLogin.Password);

    const output = await fetch(baseUrl, {
      method: "POST",
      body: form
    });
    const data = await output.json();
    const { status, title } = data;

    if(status === "ok") {
      toast.success(title);
      const { token } = data;
      dispath(setToken(token));
      navigate("/");

    } else {
      toast.error(title);
    }
  }

  return (
    <div className="login">
      <div className="form-container">
        <form className="form" onSubmit={submitHandler}>
          <h2 className="form-header">Login</h2>

          <div className="form-field">
            <input
              type="email"
              name="Email"
              id="email"
              className="email field"
              placeholder="Email"
              onChange={changeHandler}
              value={userLogin.Email}
            />
          </div>

          <div className="form-field">
            <input
              type="password"
              name="Password"
              id="password"
              className="password field"
              placeholder="Password"
              onChange={changeHandler}
              value={userLogin.Password}
            />
          </div>

          <button type="submit" className="form-btn">
            Login
          </button>
        </form>
      </div>
    </div>
  )
}

export default Login