import React from "react";
import { AiOutlineMinus, AiOutlinePlus } from "react-icons/ai";

const CartItem = (props) => {
  const {
    cartQuantity,
    imgdata,
    productDescription,
    productId,
    productName,
    productPrice,
    availableQuantity,
    updateCount,
  } = props;

  return (
    <div className="cart-item">
      <div className="section-item-image">
        <img
          src={`${imgdata}?update=${new Date().getTime()}`}
          alt="product"
          className="cart-item-image"
        />
      </div>

      <div className="section-about-item">
        <div className="about-item-desc">
          <div className="item-title-desc">
            <div className="item-title">{productName}</div>
            <div className="item-desc">{productDescription}</div>
          </div>
          <div className="item-price">₹ {productPrice}</div>
        </div>

        <div className="about-item-quantity">
          <button
            className="quantity-btn"
            onClick={() =>
              updateCount(productId, cartQuantity - 1, availableQuantity)
            }
          >
            <AiOutlineMinus />
          </button>

          <div className="update-quantity">{cartQuantity}</div>

          <button
            className="quantity-btn black"
            onClick={() =>
              updateCount(productId, cartQuantity + 1, availableQuantity)
            }
          >
            <AiOutlinePlus />
          </button>
        </div>
      </div>
    </div>
  );
};

export default CartItem;
