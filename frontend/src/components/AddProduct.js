import React, { useState } from "react";
import { useSelector } from "react-redux";
import ProductForm from "./ProductForm";
import { toast } from "react-hot-toast";
import { useNavigate } from "react-router-dom";

const baseUrl = "https://localhost:7272/api/Admin/addProduct";

const AddProduct = () => {
  const [product, setProduct] = useState({
    id: "",
    name: "",
    desc: "",
    category: "Clothing",
    availableQty: "",
    price: "",
  }); 
  
  const [error, setError] = useState({
    nameError: [],
    descError: [],
    availableQtyError: [],
    priceError: [],
    imageError: [],
  });

  const [image, setImage] = useState('');
  const navigate = useNavigate();

  const { token } = useSelector((state) => state.auth);

  const changeHandler = (event) => {
    const { name, value } = event.target;
    console.log(name, value);
    setProduct({
      ...product,
      [name]: value,
    })
  };

  const imageHandler = (event) => {
    setImage(event.target.files[0]);
  }

  const onSubmitHandler = async (event) => {
    event.preventDefault();

    try {
      let fd = new FormData();
      fd.append("Product.ProductName", product.name);
      fd.append("Product.Description", product.desc);
      fd.append("Product.Category", product.category);
      fd.append("Product.AvailableQuantity", product.availableQty);
      fd.append("Product.Price", product.price);
      fd.append("ImageData", image);
  
      const output = await fetch(`${baseUrl}`, {
        method: "POST",
        body: fd,
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      const data = await output.json();
      console.log(data);
      const { status, title } = data;

      if (status !== "ok") {
        toast.error(title);

        const { errors } = data;
        setError({
          ...error,
          imageError: errors.ImageData,
          availableQtyError: errors["Product.AvailableQuantity"],
          descError: errors["Product.Description"],
          priceError: errors["Product.Price"],
          nameError: errors["Product.ProductName"],
        });

      } else {
        toast.success(title);
        navigate("/");
      }

    } catch(error) {
      console.log(error);
    }
  };

  return (
    <div className="add-product">
      <h2>Add Product</h2>
      <ProductForm
        {...product}
        {...error}
        image={image}
        imageHandler={imageHandler}
        changeHandler={changeHandler}
        onSubmitHandler={onSubmitHandler}
      />
    </div>
  );
};

export default AddProduct;
