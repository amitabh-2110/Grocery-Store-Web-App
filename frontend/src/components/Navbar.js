import React, { useEffect } from "react";
import logo from "../assets/logo.png";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { setEmail, setFullName, setRole, setToken } from "../redux/slices/authSlice";
import { toast } from "react-hot-toast";
import { BsCart2 } from "react-icons/bs";

const baseUrl = "https://localhost:7272/api/UserAuth/fetch-user";

const Navbar = () => {
  const { token, role, fullname } = useSelector((state) => state.auth);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const FetchPerson = async () => {
    try {
      const output = await fetch(baseUrl, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      const data = await output.json();
      console.log(data);
      dispatch(setRole(data.role));
      dispatch(setFullName(data.fullName));
      dispatch(setEmail(data.email));
      
    } catch (error) {
      console.log("Couldn't make call, error: ", error);
    }
  };

  useEffect(() => {
    if (token !== null) FetchPerson();
  }, [token]);

  return (
    <div className="navbar">
      <div className="navbar-reduced-width">
        <div className="navbar-image">
          <Link to="/">
            <img src={logo} width={180} height={60} />
          </Link>
        </div>

        <div className="navbar-btn-grp">
          {token === null && (
            <Link to={"/login"} className="navbar-btn">
              Login
            </Link>
          )}

          {token === null && (
            <Link to={"/signup"} className="navbar-btn">
              Sign Up
            </Link>
          )}

          {token !== null && role !== "Admin" && (
            <div className="navbar-profile">Hello {fullname}</div>
          )}

          {token !== null && role !== "Admin" && (
            <Link to={"/cart"} className="navbar-btn">
              <BsCart2/>
            </Link>
          )}

          {token !== null && role !== "Admin" && (
            <Link to={"/orders"} className="navbar-btn">
              Orders
            </Link>
          )}

          {token !== null && (
            <button
              className="navbar-btn logout-btn"
              onClick={() => {
                toast.success("Logout Successfully");
                dispatch(setToken(null));
                navigate("/login");
              }}
            >
              Logout
            </button>
          )}
        </div>
      </div>
    </div>
  );
};

export default Navbar;
