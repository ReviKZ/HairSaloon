import { useEffect, useState, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch";

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

    const GoToUser = useCallback(async () => {
        navigate(`/details/${document.getElementById("userBox").innerText}`);
    }, []);

    return (
        <>
            <h1>Search for a user</h1>
            {loading ?
                <p>Loading...</p>
                : users ?
                    (
                        <form>
                            <input list="userlist" id="userBox"></input>
                            <datalist id="userlist">
                                {users.map((user) => 
                                    <option value={user.name}>{user.userId}</option>
                                )};
                            </datalist>
                            <button type="button" onClick={GoToUser}>Go to User</button>
                        </form>
                    )
                    :
                    (
                        <div>
                            <p>
                                There isn't any user in the database.
                            </p>
                        </div>
                    )
            }
        </>
    );
}

export default Users;