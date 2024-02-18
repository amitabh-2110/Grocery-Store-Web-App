import React from "react";
import { Link } from "react-router-dom";

const ProductCard = (props) => {
  const {
    productId,
    productName,
    price,
    imgdata,
    description,
    availableQuantity,
  } = props;

  return (
    <div className="product-card">
      <Link to={`/product/${productId}`} className="card-link">
        <h2 className="card-title">{productName}</h2>
        <p className="card-desc">{description.substr(0, 35)}</p>
        <img
          src={`${imgdata}?update=${new Date().getTime()}`}
          alt="product"
          className="card-image"
        />

        <div className="card-info">
          <div className="card-price">₹ {price}</div>
          <div className="card-product-quantity">Qty: {availableQuantity}</div>
        </div>
      </Link>
    </div>
  );
};

export default ProductCard;
