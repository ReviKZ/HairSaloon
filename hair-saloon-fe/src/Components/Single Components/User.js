import { useEffect, useState, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch";


const User = ({ id }) => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [user, setUser] = useState(false);

    useEffect(() => {
        const dataFetch = async () => {
            const data = await Fetch("get", "user/" + id, "");
            setUser(data);
            setLoading(false);
        }
        dataFetch();
    }, [{ id }]);

    const LogOut = useCallback(async () => {
        localStorage.removeItem("userId");
        navigate("/");
    }, []);

    return (
        <>
            <h1>Person Information</h1>
            {loading ?
                <p>Loading...</p>
                : user ? (
                    <div>
                        <h3>{user.firstName} {user.lastName}</h3>
                        <p>Gender: {user.gender === 0 ? <>Male</> : <>Female</>}</p>
                        <p>Position: {user.type === 0 ? <>Guest</> : <>Hairdresser</>}</p>
                        <p>Email: {user.emailAddress}</p>
                        <p>Phone: {user.phoneNumber}</p>
                        <button type="button" onClick={LogOut}>Log out!</button>
                    </div>
                ) :
                (
                    <div>
                        <p>
                            There isn't a user with this id in the database.
                        </p>
                    </div>
                )
            }       
        </>
        );
}

export default User;
