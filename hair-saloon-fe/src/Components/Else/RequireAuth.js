import { useLocation, Navigate, Outlet } from 'react-router-dom';
import { useEffect, useState } from 'react';
import TokenConverter from "./TokenConverter";
import Fetch from "../Shared Components/Fetch";



const RequireAuth = ({ allowedRoles }) => {
    const location = useLocation();
    const [user, setUser] = useState();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const dataFetch = async () => {
            const idData = await TokenConverter();
            const data = await Fetch("get", `user/${idData}`, "");
            setUser(data);
            setLoading(false);
        }
        dataFetch();
    }, []);

    return (
        localStorage?.userToken
            ? !loading
                ? allowedRoles.includes(user.type)
                    ? <Outlet />
                    : <Navigate to="/notfound" state={{ from: location }} replace />
                : <></>
        : <Navigate to="/" state={{ from: location }} replace />
    );
}

export default RequireAuth;