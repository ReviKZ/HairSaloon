import { useEffect, useState, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { Helmet } from 'react-helmet';
import Fetch from "../Shared Components/Fetch";
import { useParams } from "react-router-dom";
import "../../Styling/Appointment.css";
import TokenConverter from '../Else/TokenConverter';

const Appointment = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [appointment, setAppointment] = useState(false);
    const [hairDresser, setHairDresser] = useState(false);
    const [userId, setUserId] = useState();
    const [guest, setGuest] = useState(false);

    useEffect(() => {
        const dataFetch = async () => {
            const appointmentData = await Fetch("get", "appointment/" + id, "");
            const hairDresserData = await Fetch("get", "user/" + appointmentData.hairDresserId, "");
            const guestData = await Fetch("get", "user/" + appointmentData.guestId, "");
            const idData = await TokenConverter();
            setUserId(idData);
            setAppointment(appointmentData);
            setHairDresser(hairDresserData);
            setGuest(guestData);
            setLoading(false);
        }
        dataFetch();
    }, []);

    const verifyAppointment = useCallback(async () => {
        try { Fetch("patch", `appointment/${id}/verify`, ""); }
        catch (e) { console.log(e) }
        navigate(0);
    }, []);

    const deleteAppointment = useCallback(async () => {
        await Fetch("delete", `appointment/delete/${id}`, "");
        navigate("/appointments");
    })

    return (
        <div className="appointment-info-container">
            <Helmet><title>Appointment Details</title></Helmet>
            <h1 className="info-heading">Appointment Information</h1>
            {loading ? (
                <p className="loading-text">Loading...</p>
            ) : appointment ? (
                <div className="appointment-details">
                    <p>Date: {appointment.date.year}.{appointment.date.month < 10 ? `0${appointment.date.month}` : appointment.date.month}.{appointment.date.day < 10 ? `0${appointment.date.day}`: appointment.date.day}</p>
                    <p>Starting Time: {appointment.startTime.hour < 10 ? `0${appointment.startTime.hour}` : appointment.startTime.hour}:{appointment.startTime.minute < 10 ? `0${appointment.startTime.minute}`: appointment.startTime.minute}</p>
                    <p>Approx. Ending Time: {appointment.endTime.hour < 10 ? `0${appointment.endTime.hour}` : appointment.endTime.hour}:{appointment.endTime.minute < 10 ? `0${appointment.endTime.minute}` : appointment.endTime.minute}</p>
                    <p>Status of Appointment: {appointment.verified ? 'Verified' : 'Not Verified'}</p>
                    <p>Description of Appointment: {appointment.description}</p>
                    {userId === hairDresser.user.id ? (
                        <p>Your Guest : {guest.firstName} {guest.lastName}</p>
                    ) : (
                        <p>Your Hairdresser : {hairDresser.firstName} {hairDresser.lastName}</p>
                    )}
                    <small>Is there a problem with the appointment? You want to get in contact, make corrections?</small><br/>
                    <small>
                        You can reach your {userId === hairDresser.user.id ? (
                            <small>Guest at {guest.phoneNumber} or write to {guest.emailAddress}</small>
                        ) : (
                            <small>Hairdresser at {hairDresser.phoneNumber} or write to {hairDresser.emailAddress}</small>
                        )}
                    </small>
                    {userId === hairDresser.user.id ? (
                            <div>
                                <button className="verify-button" type="button" onClick={() => { navigate(`/appointments/edit/${id}`) }}>Edit</button>
                                <button className="delete-button" type="button" onClick={deleteAppointment}>Delete</button>
                            </div>
                    ) : (
                        <div>
                            <small>Nothing wrong? Then verify the appointment!</small><br />
                            <button className="verify-button" type="button" onClick={verifyAppointment}>
                                Verify
                            </button>
                        </div>
                    )}
                </div>
            ) : (
                <div className="no-appointment">
                    <h2>There isn't an Appointment with this id in the database.</h2>
                </div>
            )}
        </div>
    );
}

export default Appointment;