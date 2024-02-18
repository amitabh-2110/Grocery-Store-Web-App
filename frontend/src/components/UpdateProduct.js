import React, { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import ProductForm from "./ProductForm";
import { toast } from "react-hot-toast";
import { useSelector } from "react-redux";

const baseUrlProdById =
  "https://localhost:7272/api/Product/getProductById/productId";
const baseUrlProdUpdate = "https://localhost:7272/api/Admin/updateProduct";

const UpdateProduct = () => {
  const [productInfo, setProductInfo] = useState({
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

  const location = useLocation();
  const navigate = useNavigate();
  const { token } = useSelector((state) => state.auth);

  const productId = location.pathname.split("/").at(-1);

  const FetchProduct = async () => {
    const output = await fetch(`${baseUrlProdById}?productId=${productId}`);
    const data = await output.json();
    console.log(data);

    const { product } = data;

    setProductInfo({
      id: product[0].productId,
      name: product[0].productName,
      desc: product[0].description,
      category: product[0].category,
      availableQty: product[0].availableQuantity,
      price: product[0].price,
    });
  };

  const changeHandler = (event) => {
    const { name, value } = event.target;
    setProductInfo({
      ...productInfo,
      [name]: value,
    });
  };

  const imageHandler = (event) => {
    setImage(event.target.files[0]);
  }

  const onSubmitHandler = async (event) => {
    event.preventDefault();

    try {
      const fd = new FormData();
      fd.append("Product.ProductId", productInfo.id);
      fd.append("Product.ProductName", productInfo.name);
      fd.append("Product.Description", productInfo.desc);
      fd.append("Product.Category", productInfo.category);
      fd.append("Product.AvailableQuantity", productInfo.availableQty);
      fd.append("Product.Price", productInfo.price);
      fd.append("ImageData", image);
  
      const output = await fetch(`${baseUrlProdUpdate}`, {
        method: "PUT",
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
        toast.success("Product updated successfully");
        navigate(`/`);
      }

    } catch(error) {
      console.log(error);
    }
  };

  useEffect(() => {
    FetchProduct(); 
  }, []);

  return (
    <div className="update-product">
      <button className="back-btn" onClick={() => navigate(-1)}>
        Back
      </button>

      <ProductForm
        {...productInfo}
        {...error}
        imageHandler={imageHandler}
        changeHandler={changeHandler}
        onSubmitHandler={onSubmitHandler}
      />
    </div>
  );
};

export default UpdateProduct;
