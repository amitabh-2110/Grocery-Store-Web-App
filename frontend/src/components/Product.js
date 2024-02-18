import React, { useEffect, useState } from "react";
import { toast } from "react-hot-toast";
import { useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import Spinner from "./Spinner";

const baseUrlProduct = "https://localhost:7272/api/Product/getProductById";
const baseUrlProductInCart = "https://localhost:7272/api/Cart";
const baseUrlDeleteProduct = "https://localhost:7272/api/Admin";

const Product = () => {
  const [product, setProduct] = useState({
    image: "",
    item: {},
  });
  const [itemInCart, setItemInCart] = useState(false);
  const [loading, setLoading] = useState(false);

  const location = useLocation();
  const navigate = useNavigate();
  const { token, role, email } = useSelector((state) => state.auth);
  const id = location.pathname.split("/").at(-1);

  const FetchProduct = async (productId) => {
    setLoading(true);
    const outputProduct = await fetch(
      `${baseUrlProduct}/productId?productId=${productId}`, {
        cache: "reload"
      }
    );
    let outputProductInCart = 0,
      dataProductInCart = 0;

    if (token !== null && role !== "Admin") {
      outputProductInCart = await fetch(
        `${baseUrlProductInCart}/getItemPresentInCart?userId=${email}&product=${productId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      dataProductInCart = await outputProductInCart.json();
    }

    const dataProduct = await outputProduct.json();
    console.log(dataProduct);
    if (dataProductInCart !== 0) 
      setItemInCart(dataProductInCart);

    const { product, imageData } = dataProduct;
    console.log(dataProduct);
    setProduct({
      image: imageData[0].imageUrl,
      item: { ...product[0] },
    });
    setLoading(false);
  };

  const AddProductToCart = async (cartItem) => {
    const output = await fetch(`${baseUrlProductInCart}/addToCart`, {
      method: "POST",
      body: JSON.stringify(cartItem),
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });
    const data = await output.json();
    console.log(data);
  };

  const RemoveProductFromCart = async (cartItem) => {
    try {
      const output = await fetch(`${baseUrlProductInCart}/removeCartItem`, {
        method: "DELETE",
        body: JSON.stringify(cartItem),
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
      });
      const data = await output.json();
      console.log(data);
      
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    FetchProduct(id);
  }, []);

  const updateProductHandler = () => {
    navigate(`/update-product/${id}`);
  };

  const deleteProductHandler = async () => {
    const output = await fetch(`${baseUrlDeleteProduct}/deleteProduct`, {
      method: "DELETE",
      body: JSON.stringify(product.item),
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    });
    const data = await output.json();
    console.log(data);
    toast.success("Product deleted successfully");
    navigate("/");
  };

  const addToCartHandler = () => {
    if(product.item.availableQuantity > 0)  {
      const cartItem = {
        email: email,
        productId: product.item.productId,
        quantity: 1,
        price: product.item.price,
      };
      AddProductToCart(cartItem);
      toast.success("Item added successfully");
      setItemInCart(true);
      
    } else {
      toast.error("Product not available");
    }
  };

  const removeFromCartHandler = () => {
    const cartItem = {
      email: email,
      productId: product.item.productId,
      quantity: product.item.quantity,
      price: product.item.price,
    };
    RemoveProductFromCart(cartItem);
    toast.success("Item removed successfully");
    setItemInCart(false);
  };

  return (
    <div className="product">
      <button className="back-btn" onClick={() => navigate(-1)}>
        Back
      </button> 

      {loading ? (
        <Spinner />
      ) : (
        <div className="section">
          <div className="section-image">
            <img
              src={`${product.image}?update=${new Date().getTime()}`}
              alt="product" 
              className="section-product-image"
            />

            {token !== null && (
              <div className="section-image-operation">
                {role === "Admin" && (
                  <div className="btn-grp">
                    <button className="btn" onClick={updateProductHandler}>
                      Update product
                    </button>

                    <button className="btn" onClick={deleteProductHandler}>
                      Delete product
                    </button>
                  </div>
                )}

                {role !== "Admin" && (itemInCart ? (
                  <button className="btn" onClick={removeFromCartHandler}>
                    Remove from cart
                  </button>
                ) : (
                  <button className="btn" onClick={addToCartHandler}>
                    Add to cart
                  </button>
                ))}
              </div>
            )}
          </div>

          <div className="section-product-info">
            <div className="prod-name">{product.item.productName}</div>
            <p className="prod-desc">{product.item.description}</p>
            <p className="prod-category">
              Category -{" "}
              <span className="prod-cat-span">{product.item.category}</span>
            </p>
            <div className="prod-price">₹ {product.item.price}</div>
          </div>
        </div>
      )}
    </div>
  );
};

export default Product;
