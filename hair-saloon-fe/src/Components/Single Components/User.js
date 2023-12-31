import { useEffect, useState, useCallback } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Helmet } from 'react-helmet';
import { FaMars, FaVenus, FaGenderless, FaEnvelope, FaPhone, FaPen, FaTrash } from "react-icons/fa";
import Fetch from "../Shared Components/Fetch";
import TokenConverter from "../Else/TokenConverter";
import "../../Styling/User.css";

const User = () => {
    const { personId } = useParams();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [user, setUser] = useState(false);
    const [id, setId] = useState();
    const [currUser, setCurrUser] = useState();

    useEffect(() => {
        const dataFetch = async () => {
            const idData = await TokenConverter();
            const userData = await Fetch("get", `user/${personId ? personId : idData}`, "");
            const currUserData = await Fetch("get", `user/${idData}`, "");
            setCurrUser(currUserData);
            setId(idData);
            setUser(userData);
            setLoading(false);
        }
        dataFetch();
    }, []);

    const LogOut = useCallback(async () => {
        await localStorage.removeItem("userToken");
        await navigate(0);
    }, []);

    const deleteUser = useCallback(async () => {
        await Fetch("delete", `user/delete/${personId ? personId : id}`, "");
        if (id === user.user.id) {
            LogOut()
        }
        else {
            navigate("/");
        }
    })

    return (
        <div className="person-info-container">
            <Helmet><title>User Details</title></Helmet>
            <h1 className="info-heading">Person Information</h1>
            {loading ? (
                <p className="loading-text">Loading...</p>
            ) : user ? (
                <div className="user-details">
                    <h3>{user.firstName} {user.lastName}</h3>
                        <p className="gender">Gender: {user.gender === 0 ? <FaMars className="icon" /> : user.gender === 1 ? <FaVenus className="icon" /> : <FaGenderless className="icon" />}</p>
                    <p className="position">Position: {user.type === 0 ? 'Guest' : 'Hairdresser'}</p>
                        <p className="email"><FaEnvelope className="icon" /> {user.emailAddress}</p>
                        <p className="phone"><FaPhone className="icon" /> {user.phoneNumber}</p>

                        {currUser.type === 1 ? <div>
                            <button className="verify-button" type="button" onClick={() => { navigate(`/edit/${personId ? personId : id}`) }}><FaPen className="icon" /></button>
                            <button className="delete-button" type="button" onClick={deleteUser}><FaTrash className="icon" /></button>
                        </div> :
                            <></>}
                </div>
            ) : (
                <div className="no-user">
                    <p className="no-user-text">There isn't a user with this id in the database.</p>
                </div>
            )}
        </div>
        );
}

export default User;
