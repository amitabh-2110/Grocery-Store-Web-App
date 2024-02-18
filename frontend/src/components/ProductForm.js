import React from "react";
import { FaRupeeSign } from "react-icons/fa";

const ProductForm = (props) => {
  const {
    name,
    desc,
    category,
    availableQty,
    price,
    image,
    nameError,
    descError,
    availableQtyError,
    priceError,
    imageError,
    imageHandler,
    changeHandler,
    onSubmitHandler,
  } = props;

  console.log(props);

  return (
    <div className="product-form">
      <div className="form-container">
        <form onSubmit={onSubmitHandler} className="form">
          <div className="form-prod">
            <input
              type="text"
              id="name"
              className="name field"
              name="name"
              placeholder="Enter product title"
              onChange={changeHandler}
              value={name}
            />
            <span className="form-error">{nameError}</span>
          </div>

          <div className="form-prod">
            <textarea
              className="desc field"
              id="desc"
              name="desc"
              onChange={changeHandler}
              value={desc}
              placeholder="Enter product description"
            ></textarea>
            <span className="form-error">{descError}</span>
          </div>

          <select
            className="category-item field"
            id="category"
            value={category}
            name="category"
            onChange={changeHandler}
          >
            <option value="Clothing">Clothing</option>
            <option value="Electronics">Electronics</option>
            <option value="Food">Food</option>
            <option value="Cups">Cups</option>
          </select>

          <div className="form-prod">
            <input
              type="number"
              placeholder="Enter available quantity"
              id="availableQty"
              name="availableQty"
              className="availableQty field"
              onChange={changeHandler}
              value={availableQty}
            />
            <span className="form-error">{availableQtyError}</span>
          </div>

          <div className="form-prod">
            <FaRupeeSign className="rupee-symbol" />
            <input
              type="number"
              placeholder="Enter price"
              id="price"
              name="price"
              className="price field"
              onChange={changeHandler}
              value={price}
            />
            <span className="form-error">{priceError}</span>
          </div>

          <div className="form-prod">
            <input
              type="file"
              id="image"
              name="image field"
              className="image"
              onChange={imageHandler}
            />
            <span className="form-error">{imageError}</span>
          </div>

          <button type="submit" className="form-btn">
            Submit product
          </button>
        </form>
      </div>
    </div>
  );
};

export default ProductForm;
