import { useEffect, useState, useCallback } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch";
import "../../Styling/User.css";

const User = ({ id }) => {
    const { personId } = useParams();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [user, setUser] = useState(false);

    useEffect(() => {
        const dataFetch = async () => {
            const data = await Fetch("get", `user/${id ? id : personId}`, "");
            setUser(data);
            setLoading(false);
        }
        dataFetch();
    }, []);

    const LogOut = useCallback(async () => {
        localStorage.removeItem("userId");
        navigate("/");
    }, []);

    const GoToAppointments = useCallback(async () => {
        navigate("/appointments");
    }, []);

    return (
        <div className="person-info-container">
            <h1 className="info-heading">Person Information</h1>
            {loading ? (
                <p className="loading-text">Loading...</p>
            ) : user ? (
                <div className="user-details">
                    <h3>{user.firstName} {user.lastName}</h3>
                    <p className="gender">Gender: {user.gender === 0 ? 'Male' : 'Female'}</p>
                    <p className="position">Position: {user.type === 0 ? 'Guest' : 'Hairdresser'}</p>
                    <p className="email">Email: {user.emailAddress}</p>
                    <p className="phone">Phone: {user.phoneNumber}</p>
                    {id === localStorage.getItem("userId") ?
                        (<>
                            <button className="logout-button" type="button" onClick={LogOut}>Log out!</button><br /><br />
                            <button className="logout-button" type="button" onClick={GoToAppointments}>Appointments</button>
                         </>
                        ) 
                            :
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
