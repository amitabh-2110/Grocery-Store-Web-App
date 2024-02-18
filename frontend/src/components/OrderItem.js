import React from "react";

const OrderItem = (props) => {
  const { imgdata, orderId, price, productId, quantity, dateOfOrder } = props;
  console.log(orderId);

  return (
    <div className="order-item">
      <div className="order-image">
        <img
          src={`${imgdata}?update=${new Date().getTime()}`}
          alt="product"
          className="order-item-image"
        />
      </div>

      <div className="order-desc">
        <div className="product-desc">
          {/* <div className="order-prod-id">Product ID - {productId}</div> */}
          <div className="order-id">
            Order ID - <br /> {orderId}
          </div>
          <div className="order-prod-quant">{quantity} items</div>
          <div className="order-prod-price">₹ {price}</div>
        </div>

        <div className="order-date">{dateOfOrder.substr(0, 10)}</div>
      </div>
    </div>
  );
};

export default OrderItem;
