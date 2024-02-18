import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import Spinner from "./Spinner";
import CartItem from "./CartItem";
import { useNavigate } from "react-router-dom";
import { toast } from "react-hot-toast";

const baseUrl = "https://localhost:7272/api/Cart";
const baseUrlOrder = "https://localhost:7272/api/Orders";

const Cart = () => {
  const [products, setProducts] = useState({
    prods: [],
    images: [],
  });
  const [cartStatus, setCartStatus] = useState(false);
  const [loading, setLoading] = useState(false);
  const [count, setCount] = useState(0);
  const { token, email } = useSelector((state) => state.auth);
  const navigate = useNavigate();

  const FetchCartItems = async (email) => {
    setLoading(true);
    const output = await fetch(`${baseUrl}/getCartItems?userId=${email}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    const data = await output.json();
    const { status } = data;

    if (status === "ok") {
      const { products, images } = data;
      setProducts({ prods: [...products], images: [...images] });
      setCartStatus(true);
    } else {
      setCartStatus(false);
    }
    setLoading(false);
  };

  const setImgProd = () => {
    const arr = [];

    products.prods.forEach((item) => {
      products.images.forEach((img) => {
        if (item.productId === img.productId) {
          const imgdata = img.imageUrl;
          arr.push({
            ...item,
            imgdata,
          });
        }
      });
    });

    return arr;
  };

  const updateCount = async (productId, count, avilableQuantity) => {
    console.log(`${count} ${avilableQuantity}`);
    if(count > 0 && count <= avilableQuantity) {
      const output = await fetch(
        `${baseUrl}/updateQuantity?userId=${email}&product=${productId}&quantity=${count}`,
        {
          method: "PUT",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      const data = await output.json();
      console.log(data);
      setCount(count);
    }
  };

  const submitHandler = async () => {
    setLoading(true);
    const output = await fetch(`${baseUrlOrder}/placeOrder?userId=${email}`, {
      method: "GET",
      headers: {
        'Authorization': `Bearer ${token}`,
      }
    });
    const data = await output.json();
    console.log(data);
    toast.success("Order placed successfully");
    navigate("/orders");
    setLoading(false);
  };

  useEffect(() => {
    FetchCartItems(email);
  }, [count]);

  return (
    <div className="cart">
      {loading ? (
        <Spinner />
      ) : (
        <div className="cart-section">
          {!cartStatus ? (
            <div className="cart-not-found">
              <h2 className="not-found-header">No item in cart was found</h2>
              <button className="btn" onClick={() => navigate("/")}>Shop now</button>
            </div>
          ) : (
            <div className="cart-found">
              <div className="cart-items">
                {setImgProd().map((item, index) => (
                  <CartItem key={index} updateCount={updateCount} {...item} />
                ))}
              </div>

              <button className="place-order-btn" onClick={submitHandler}>
                Place Order
              </button>
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default Cart;
