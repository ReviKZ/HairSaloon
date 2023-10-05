import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Helmet } from 'react-helmet';
import Fetch from '../Shared Components/Fetch';
import "../../Styling/AppointmentForm.css";

const AppointmentForm = () => {
    const navigate = useNavigate();
    const [form, updateForm] = useState({});

    const updateField = (newValue, field) => {
        updateForm({ ...form, [field]: newValue })
    }

    const [loading, setLoading] = useState(true);
    const [guests, setGuest] = useState(false);
    const [hds, setHd] = useState(false);

    useEffect(() => {
        const dataFetch = async () => {
            const guestData = await Fetch("get", "user/list", "");
            const hdData = await Fetch("get", "user/list/hairdressers", "");
            setGuest(guestData);
            setHd(hdData);
            setLoading(false);
        }
        dataFetch();
    }, []);

    async function AddAppointment(event) {
        event.preventDefault();
        var dateList = await form["Date"].split("-");
        var sTList = await form["StartTime"].split(":");
        var eTList = await form["EndTime"].split(":");
        var formData = {
            date: {
                year: parseInt(dateList[0]),
                month: parseInt(dateList[1]),
                day: parseInt(dateList[2])
            },
            startTime: {
                hour: parseInt(sTList[0]),
                minute: parseInt(sTList[1]),
                second: 0
            },
            endTime: {
                hour: parseInt(eTList[0]),
                minute: parseInt(eTList[1]),
                second: 0
            },
            guestId: guests.find((g) => g.name === form["Guest"]).userId,
            hairDresserId: hds.find((h) => h.name === form["HairDresser"]).userId,
            description: form["Description"]
        };

        const result = await Fetch("post", "appointment/create", formData);
        result == false ?
            alert("Something went wrong! ")
            :
            alert(result.message);
        navigate(0);

    };


    return (
        <div className="container">
            <Helmet><title>Create Appointment</title></Helmet>
            {loading ? (
                <p>Loading...</p>
            ) : guests ? (
                <>
                    <h1>Add an appointment!</h1>

                    <form onSubmit={AddAppointment} className="appointment-form">
                        <div className="form-field">
                            <p>Date: </p>
                            <input
                                onChange={(e) => updateField(e.target.value, "Date")}
                                id="Date"
                                type="date"
                                min="1900-01-01"
                                max="2099-12-31"
                            />
                        </div>
                        <div className="form-field">
                            <p>Start Time: </p>
                            <input
                                onChange={(e) => updateField(e.target.value, "StartTime")}
                                id="StartTime"
                                type="time"
                            />
                        </div>
                        <div className="form-field">
                            <p>Approx. End Time: </p>
                            <input
                                onChange={(e) => updateField(e.target.value, "EndTime")}
                                id="EndTime"
                                type="time"
                            />
                        </div>
                        <div className="form-field">
                            <p>Guest: </p>
                            <input
                                onChange={(e) => updateField(e.target.value, "Guest")}
                                list="guestList"
                                id="Guest"
                            />
                            <datalist id="guestList">
                                {guests.map((guest) => (
                                    <option key={guest.userId} value={guest.name} data-id={guest.userId}></option>
                                ))}
                            </datalist>
                        </div>
                        <div className="form-field">
                            <p>Hairdresser: </p>
                            <input
                                onChange={(e) => updateField(e.target.value, "HairDresser")}
                                list="hdList"
                                id="HairDresser"
                            />
                            <datalist id="hdList">
                                {hds.map((hd) => (
                                    <option key={hd.userId} value={hd.name} data-id={hd.userId}></option>
                                ))}
                            </datalist>
                        </div>
                        <div className="form-field">
                            <p>Description: </p>
                            <input
                                onChange={(e) => updateField(e.target.value, "Description")}
                                id="Description"
                                type="text"
                            />
                        </div>
                        <button type="submit" className="submit-button">Create Appointment</button>
                    </form>
                </>
            ) : (
                <div className="error-message">
                    <p>Something went wrong.</p>
                </div>
            )}
        </div>
    );
};

export default AppointmentForm;