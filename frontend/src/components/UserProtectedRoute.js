import { useSelector } from 'react-redux'
import { Navigate, useNavigate } from 'react-router-dom';

const UserProtectedRoute = ({children}) => {
  const {token, role} = useSelector((state) => state.auth);

  if(token !== null && role === "User") {
    return children;
  } else if(token !== null) {
    return <Navigate to="/" />
  } else {
    return <Navigate to="/login" />
  }
}

export default UserProtectedRoute