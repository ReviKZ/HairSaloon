import { useEffect, useState, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { Helmet } from 'react-helmet';
import Fetch from "../Shared Components/Fetch";
import "../../Styling/Users.css"

const Users = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [users, setUsers] = useState(false);

    useEffect(() => {
        const dataFetch = async () => {
            const data = await Fetch("get", "user/list", "");
            setUsers(data);
            setLoading(false);
        }
        dataFetch();
    }, []);

    const GoToUser = useCallback(() => {
        var selectedUser = document.getElementById("userBox").value;
        if (selectedUser == "") {
            alert("Please choose a user!")
            return;
        }
        var selectedId = document.querySelector(`#userlist option[value='${selectedUser}']`).dataset.id
        navigate(`/details/${selectedId}`);
    }, []);

    return (
        <div className="user-search-container">
            <Helmet><title>User Searcher</title></Helmet>
            <h1>Search for a user</h1>
            {loading ? (
                <p>Loading...</p>
            ) : users ? (
                <form className="search-form">
                    <input list="userlist" id="userBox" className="search-input" />
                    <datalist id="userlist">
                        {users.map((user) => (
                            <option key={user.userId} data-id={user.userId} value={user.name}></option>
                        ))}
                    </datalist>
                    <button type="button" onClick={GoToUser} className="search-button">
                        Go to User
                    </button>
                </form>
            ) : (
                <div className="no-user-message">
                    <p>There isn't any user in the database.</p>
                </div>
            )}
        </div>
    );
}

export default Users;