import { Route, Routes } from 'react-router-dom';
import './App.css';
import Navbar from './components/Navbar';
import Home from './components/Home';
import Product from './components/Product'
import Cart from './components/Cart'
import Orders from './components/Orders'
import Login from './components/Login'
import Signup from './components/Signup'
import UpdateProduct from './components/UpdateProduct';
import AddProduct from './components/AddProduct';
import UserProtectedRoute from './components/UserProtectedRoute';
import AdminProtectedRoute from './components/AdminProtectedRoute';

function App() {
  return (
    <div className="App">
      <Navbar />

      <Routes>
        <Route path="/" element={<Home/>} />

        <Route path="/product/:id" element={<Product/>} />

        <Route path="/cart" element={
          <UserProtectedRoute>
            <Cart />
          </UserProtectedRoute>
        } />

        <Route path="/add-product" element={
          <AdminProtectedRoute>
            <AddProduct />
          </AdminProtectedRoute>
        } />

        <Route path="/update-product/:id" element={
          <AdminProtectedRoute>
            <UpdateProduct/>
          </AdminProtectedRoute>
        } />

        <Route path="/orders" element={
          <UserProtectedRoute>
            <Orders />
          </UserProtectedRoute>
        } />

        <Route path="/signup" element={<Signup/>} />
        
        <Route path="/login" element={<Login/>} />
      </Routes>
    </div>
  );
}

export default App;
