import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch";

const Users = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [users, setUsers] = useState(false);

    useEffect(() => {
        const dataFetch = async () => {
            const data = await Fetch("get", "list", "");
            setUsers(data);
            setLoading(false);
        }
        dataFetch();
    }, []);

    return (
        <>
            <h1>Search for a user</h1>
            {loading ?
                <p>Loading...</p>
                : users ?
                    (
                        <form>
                            <input list="userlist" id="selectedUser" />
                                <datalist id="userlist">
                                    {users.map((user) => 
                                        <option value={user.userId}>{user.name}</option>
                                    )};
                                </datalist>
                                <input type="button" onClick={navigate("/details/" + document.getElementById("selectedUser").value)}>Go to User</input>
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