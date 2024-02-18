import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import Spinner from "./Spinner";
import { useNavigate } from "react-router-dom";
import OrderItem from "./OrderItem";

const baseUrl = "https://localhost:7272/api/Orders";

const Orders = () => {
  const [products, setProducts] = useState({
    productInfo: [],
    images: [],
  });
  const [loading, setLoading] = useState(false);
  const [orderStatus, setOrderStatus] = useState(false);
  const { token, email } = useSelector((state) => state.auth);
  const navigate = useNavigate();

  const FetchOrders = async () => {
    setLoading(true);

    const output = await fetch(`${baseUrl}/getOrders?userId=${email}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    const data = await output.json();
    const { products, imageData } = data;

    if (products.length > 0) {
      setProducts({ productInfo: products, images: imageData });
      setOrderStatus(true);
    } else {
      setOrderStatus(false);
    }

    setLoading(false);
  };

  const setImgProd = () => {
    const arr = [];

    products.productInfo.forEach((item) => {
      products.images.forEach((img) => {
        if (item.orderId === img.orderId) {
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

  useEffect(() => {
    FetchOrders();
  }, []);

  return (
    <div className="orders">
      {loading ? (
        <Spinner />
      ) : (
        <div className="section-orders">
          {!orderStatus ? (
            <div className="orders-not-found">
              <h2 className="header">No orders found</h2>
              <button className="btn" onClick={() => navigate("/")}>
                Shop now
              </button>
            </div>
          ) : (
            <div className="orders-items">
              {setImgProd().map((item) => {
                return <OrderItem key={item.orderId} {...item} />;
              })}
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default Orders;
