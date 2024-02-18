import React, { useEffect, useState } from "react";
import Spinner from "./Spinner";
import ProductCard from "./ProductCard";
import { AiOutlineSearch } from "react-icons/ai";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

const baseUrl = "https://localhost:7272/api/Product";

const Home = () => {
  // products container

  const [products, setProducts] = useState({
    imageInfo: [],
    productInfo: [],
  });
  const [findProducts, setFindProducts] = useState({
    category: "All",
    search: "",
  });
  const [loading, setLoading] = useState(false);
  const { role, token } = useSelector((state) => state.auth);
  const navigate = useNavigate();

  const setImgProd = () => {
    const arr = [];

    products.productInfo.forEach((prod) => {
      products.imageInfo.forEach((img) => {
        if (prod.productId === img.productId) {
          const imgdata = img.imageUrl;
          arr.push({
            ...prod,
            imgdata,
          });
        }
      });
    });

    return arr;
  };

  const FetchProducts = async () => {
    setLoading(true);
    const output = await fetch(`${baseUrl}/getAllProducts`, {
      cache: "reload"
    });
    const data = await output.json();
    const { imageData, product } = data;
    setProducts({ imageInfo: imageData, productInfo: product });
    setLoading(false);
  };

  const changeHandler = (event) => {
    setFindProducts({
      ...findProducts,
      [event.target.name]: event.target.value,
    });
  }

  const submitHandler = async () => {
    setLoading(true);
    console.log(findProducts);
    const output = await fetch(`${baseUrl}/getProductsByCategorySearch?categoryId=${findProducts.category}&search=${findProducts.search}`);
    const data = await output.json();
    const { imageData, product } = data;
    setProducts({ imageInfo: imageData, productInfo: product });
    setLoading(false);
  }

  const addProductHandler = async () => {
    navigate("/add-product");
  }

  useEffect(() => {
    FetchProducts();
  }, []);

  return (
    <div className="home">
      <div className="home-header">

        <select
          className="select-category"
          name="category"
          onChange={changeHandler}
          value={findProducts.category}
        >
          <option value="All">Select category</option>
          <option value="Clothing">Clothing</option>
          <option value="Electronics">Electronics</option>
          <option value="Food">Food</option>
          <option value="Cups">Cups</option>
        </select>

        <div className="header-search">
          <AiOutlineSearch className="search-icon" />
          <input
            type="text"
            placeholder="Search"
            name="search"
            className="search"
            value={findProducts.search}
            onChange={changeHandler}
          />
        </div>

        <button className="submit-cat-btn" onClick={submitHandler}>
          Search product
        </button>

        {token !== null && role === "Admin" && (
          <button className="header-btn" onClick={addProductHandler}>Add Product</button>
        )}
      </div>

      {loading ? (
        <Spinner />
      ) : (
        <div className="product-cards">
          {setImgProd().map((prod) => (
            <ProductCard key={prod.productId} {...prod} />
          ))}
        </div>
      )}
    </div>
  );
};

export default Home;
