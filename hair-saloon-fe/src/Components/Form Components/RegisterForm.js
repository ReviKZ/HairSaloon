import "../../Styling/Register.css";
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch"
import "../../Styling/LoginForm.css";

const RegisterForm = () => {
    const navigate = useNavigate();
    const [validationErrors, setValidationErrors] = useState({});

    const [form, updateForm] = useState({});

    const updateField = (newValue, field) => {
        updateForm({ ...form, [field]: newValue })

        const errors = { ...validationErrors };

        if (field === 'UserName' && newValue.length < 6) {
            errors.UserName = 'Username should be at least 6 characters';
        } else if (
            (field === 'Password') &&
            newValue.length < 6
        ) {
            errors.Password = 'Password should be at least 6 characters';
        } else if (
            field === 'ConfirmPassword' &&
            newValue !== form.Password
        ) {
            errors.ConfirmPassword = 'Password and Confirm Password do not match';
        } else if (field === 'PhoneNumber' && !/^\+?\d+$/.test(newValue)) {
            errors.PhoneNumber = 'Phone Number should only contain numbers';
        } else if (
            field === 'EmailAddress' &&
            !/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(newValue)
        ) {
            errors.EmailAddress = 'Email Address should be in a valid format';
        } else {
            // Clear the error if the validation passes
            delete errors[field];
        }

        setValidationErrors(errors);
    };

    async function Register(event) {
        event.preventDefault();
        var formData = {
            user: {
                userName: form["UserName"],
                password: form["Password"],
                confirmPassword: form["ConfirmPassword"]
            },
            person: {
                firstName: form["FirstName"],
                lastName: form["LastName"],
                phoneNumber: form["PhoneNumber"],
                emailAddress: form["EmailAddress"]
            },
            gender: form["Gender"]? form["Gender"] : "Male",
            personType: form["PersonType"]? form["PersonType"] : "HairDresser"
        };

        const result = await Fetch("post", "user/register", formData);
        result == false ?
            alert("A user already exists with this username!")
            :
            alert(result.message);
        navigate(0);
    }
    return (
        <div className="container">
            <h1 className="header">Register a User!</h1>

            <form>
                <div className="inputContainer">
                    <p className="label">Username: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "UserName")} id="UserName" type="text" />
                    {validationErrors.UserName && (
                        <p className="error">{validationErrors.UserName}</p>
                    )}
                </div>
                <div className="inputContainer">
                    <p className="label">Password: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "Password")} id="Password" type="password" />
                    {validationErrors.Password && (
                        <p className="error">{validationErrors.Password}</p>
                    )}
                </div>
                <div className="inputContainer">
                    <p className="label">Confirm Password: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "ConfirmPassword")} id="ConfirmPassword" type="password" />
                    {validationErrors.ConfirmPassword && (
                        <p className="error">{validationErrors.ConfirmPassword}</p>
                    )}
                </div>
                <div className="inputContainer">
                    <p className="label">First name: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "FirstName")} id="FirstName" type="text" />
                </div>
                <div className="inputContainer">
                    <p className="label">Last name: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "LastName")} id="LastName" type="text" />
                </div>
                <div className="inputContainer">
                    <p className="label">Phone Number: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "PhoneNumber")} id="PhoneNumber" type="text" />
                    {validationErrors.PhoneNumber && (
                        <p className="error">{validationErrors.PhoneNumber}</p>
                    )}
                </div>
                <div className="inputContainer">
                    <p className="label">Email Address: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "EmailAddress")} id="EmailAddress" type="text" />
                    {validationErrors.EmailAddress && (
                        <p className="error">{validationErrors.EmailAddress}</p>
                    )}
                </div>
                <div className="inputContainer">
                    <p className="label">Gender: </p>
                    <select className="input" onChange={(e) => updateField(e.target.value, "Gender")} id="Gender">
                        <option selected value="Male">Male</option>
                        <option value="Female">Female</option>
                        <option value="Else">None of the above</option>
                    </select>
                </div>
                <div className="inputContainer">
                    <p className="label">Position: </p>
                    <select className="input" onChange={(e) => updateField(e.target.value, "PersonType")} id="PersonType">
                        <option selected value="HairDresser">HairDresser</option>
                        <option value="Guest">Guest</option>
                    </select>
                </div>


                <button className="button" type="submit" onClick={Register}>Register User</button>

            </form>

        </div>
    );

};

export default RegisterForm;