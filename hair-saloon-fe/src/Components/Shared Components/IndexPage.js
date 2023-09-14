import LoginForm from "../Form Components/LoginForm";
import { Navigate } from 'react-router-dom';

const IndexPage = () => {
    return (
        <>
            {localStorage.getItem("userToken") == null ? <LoginForm /> :
                <Navigate to="/details"></Navigate>
            }
        </>    
    );

};

export default IndexPage;