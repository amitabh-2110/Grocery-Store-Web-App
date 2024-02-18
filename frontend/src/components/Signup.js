import React, { useState } from "react";
import { toast } from "react-hot-toast";

const baseUrl = "https://localhost:7272/api/UserAuth/signup";

const Signup = () => {
  const [user, setUser] = useState({
    FullName: "",
    Email: "",
    PhoneNumber: "",
    Password: "",
    Role: "User",
  });

  const [error, setError] = useState({
    fullnameError: [],
    emailError: [],
    phoneNumberError: [],
    passwordError: [],
  });

  const submitHandler = async (event) => {
    event.preventDefault();
    let form = new FormData();

    form.append("Email", user.Email);
    form.append("FullName", user.FullName);
    form.append("PhoneNumber", user.PhoneNumber);
    form.append("Role", user.Role);
    form.append("Password", user.Password);

    const output = await fetch(baseUrl, {
      method: "POST",
      body: form,
    });
    const data = await output.json();
    const { status, title } = data;

    if (status !== "ok") {
      toast.error(title);

      const { errors } = data;
      setError({
        ...error,
        emailError: errors.Email,
        phoneNumberError: errors.PhoneNumber,
        fullnameError: errors.FullName,
        passwordError: errors.Password,
      });

    } else {
      toast.success(title);
      toast.success("Please login");
    }
  };

  const changeHandler = (event) => {
    setUser({
      ...user,
      [event.target.name]: event.target.value,
    });
  };

  return (
    <div className="signup">
      <div className="form-container">
        <form className="form" onSubmit={submitHandler}>
          <h2 className="form-header">Signup</h2>

          <div className="form-field">
            <input
              type="text"
              name="FullName"
              id="fullname"
              className="fullname field"
              placeholder="Full Name"
              onChange={changeHandler}
              value={user.FullName}
            />
            <div className="form-error">{error.fullnameError}</div>
          </div>

          <div className="form-field">
            <input
              type="email"
              name="Email"
              id="email"
              className="email field"
              placeholder="Email"
              onChange={changeHandler}
              value={user.Email}
            />
            <div className="form-error">{error.emailError}</div>
          </div>

          <div className="form-field">
            <input
              type="text"
              name="PhoneNumber"
              id="phoneNumber"
              className="phoneNumber field"
              placeholder="Phone Number"
              onChange={changeHandler}
              value={user.PhoneNumber}
            />
            <div className="form-error">{error.phoneNumberError}</div>
          </div>

          <div className="form-field">
            <input
              type="password"
              name="Password"
              id="password"
              className="password field"
              placeholder="Password"
              onChange={changeHandler}
              value={user.Password}
            />
            <div className="form-error">{error.passwordError}</div>
          </div>

          <button type="submit" className="form-btn">
            Signup
          </button>
        </form>
      </div>
    </div>
  );
};

export default Signup;
