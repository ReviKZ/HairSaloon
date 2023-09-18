import { useEffect, useState, useCallback } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch";
import TokenConverter from "../Else/TokenConverter";
import "../../Styling/User.css";

const EditUserForm = () => {
    const { personId } = useParams();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [user, setUser] = useState(false);
    const [id, setId] = useState();
    const [validationErrors, setValidationErrors] = useState({});
    const [form, updateForm] = useState({});

    const updateField = (newValue, field) => {
        updateForm({ ...form, [field]: newValue })

        const errors = { ...validationErrors };

        if (field === 'PhoneNumber' && !/^\+?\d+$/.test(newValue)) {
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


    useEffect(() => {
        const dataFetch = async () => {
            const idData = await TokenConverter();
            const data = await Fetch("get", `user/${personId ? personId : idData}`, "");
            setId(idData);
            setUser(data);
            setLoading(false);
        }
        dataFetch();
    }, []);

    async function EditUser(event) {
        event.preventDefault();
        var formData = {
            firstName: form["FirstName"] ? form["FirstName"] : user.firstName,
            lastName: form["LastName"] ? form["LastName"] : user.lastName,
            phoneNumber: form["PhoneNumber"] ? form["PhoneNumber"] : user.phoneNumber,
            emailAddress: form["EmailAddress"] ? form["EmailAddress"] : user.emailAddress
        };

        const result = await Fetch("put", `user/edit/${personId ? personId : id}`, formData);
        result == false ?
            alert("Something went wrong with editing!")
            :
            alert(result.message);
        navigate(0);
    }

    return (
        <div className="container">
            <h1 className="header">Edit this User!</h1>

            <form>
                <div className="inputContainer">
                    <p className="label">First name: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "FirstName")} id="FirstName" type="text" placeholder={user.firstName} />
                </div>
                <div className="inputContainer">
                    <p className="label">Last name: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "LastName")} id="LastName" type="text" placeholder={user.lastName} />
                </div>
                <div className="inputContainer">
                    <p className="label">Phone Number: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "PhoneNumber")} id="PhoneNumber" type="text" placeholder={user.phoneNumber} />
                    {validationErrors.PhoneNumber && (
                        <p className="error">{validationErrors.PhoneNumber}</p>
                    )}
                </div>
                <div className="inputContainer">
                    <p className="label">Email Address: </p>
                    <input className="input" onChange={(e) => updateField(e.target.value, "EmailAddress")} id="EmailAddress" type="text" placeholder={user.emailAddress} />
                    {validationErrors.EmailAddress && (
                        <p className="error">{validationErrors.EmailAddress}</p>
                    )}
                </div>


                <button className="button" type="submit" onClick={EditUser}>Edit User</button>

            </form>

        </div>
    );
};

export default EditUserForm;