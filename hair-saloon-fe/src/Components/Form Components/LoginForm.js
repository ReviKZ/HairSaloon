import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch"

const LoginForm = () => {
    const navigate = useNavigate();

    const [form, updateForm] = useState({});

    const updateField = (newValue, field) => {
        updateForm({ ...form, [field]: newValue })
    }

    async function Login(event) {
        event.preventDefault();
        var formData = {
            UserName: form["UserName"],
            Password: form["Password"]
        };

        const userId = await Fetch("post", "user/login", formData);
        localStorage.setItem("userId", userId);
        navigate("/details");
    }
        return (
            <>
                <h1>Please log in!</h1>

                <form>
                    <div>
                        <p>Username: </p>
                        <input onChange={(e) => updateField(e.target.value, "UserName")} id="UserName" type="text" />
                    </div>
                    <div>
                        <p>Password: </p>
                        <input onChange={(e) => updateField(e.target.value, "Password")} id="Password" type="password" />
                    </div>


                    <button type="submit" onClick={Login}>Login</button>

                </form>

            </>
        );

    };

export default LoginForm;