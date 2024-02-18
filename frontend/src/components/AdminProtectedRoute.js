import { useSelector } from 'react-redux'
import { Navigate, useNavigate } from 'react-router-dom';

const AdminProtectedRoute = ({children}) => {
  const {token, role} = useSelector((state) => state.auth);

  if(token !== null && role === "Admin") {
    return children;
  } else if(token !== null) {
    return <Navigate to="/" />
  } else {
    return <Navigate to="/login" />
  }
}

export default AdminProtectedRoute