import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch"
import "../../Styling/LoginForm.css";

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

        const userToken = await Fetch("post", "user/login", formData);
        if (!userToken) {
            alert("Wrong username & password combination!")
        }
        else {
            await localStorage.setItem("userToken", userToken.token)
            navigate(0)
        }

    };
        return (
            <div className="container">
                <h1 className="header">Please log in!</h1>

                <form>
                    <div className="inputContainer">
                        <p className="label">Username: </p>
                        <input className="input" onChange={(e) => updateField(e.target.value, "UserName")} id="UserName" type="text" />
                    </div>
                    <div className="inputContainer">
                        <p className="label">Password: </p>
                        <input className="input" onChange={(e) => updateField(e.target.value, "Password")} id="Password" type="password" />
                    </div>


                    <button className="button" type="submit" onClick={Login}>Login</button>

                </form>

            </div>
        );

    };

export default LoginForm;