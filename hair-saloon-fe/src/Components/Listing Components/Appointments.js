import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Helmet } from 'react-helmet';
import Fetch from "../Shared Components/Fetch";
import PersonConverter from "../Else/PersonConverter";
import "../../Styling/Appointments.css";
import TokenConverter from '../Else/TokenConverter';


const Appointments = () => {
    const [loading, setLoading] = useState(true);
    const [appointments, setAppointments] = useState(false);
    const [id, setId] = useState();

    useEffect(() => {
        const dataFetch = async () => {
            const idData = await TokenConverter();
            const appointmentData = await Fetch("get", "appointment/list/" + idData, "");
            setId(idData);
            setAppointments(appointmentData);
            setLoading(false);
        }
        dataFetch();
    }, []);

    return (
        <div className="appointments-container">
            <Helmet><title>Appointment List</title></Helmet>
            <h1 className="appointments-heading">Appointments</h1>
            {loading ? (
                <p className="loading-text">Loading...</p>
            ) : appointments ? (
                <div className="appointment-list">
                    {appointments.map((appointment) => (
                        <div key={appointment.id} className="appointment-item">
                            <Link to={`/appointments/${appointment.id}`} className="appointment-link">
                                <div className="appointment-details">
                                    {`${appointment.date.year}.${appointment.date.month < 10 ? "0" + appointment.date.month : appointment.date.month}.${appointment.date.day < 10 ? "0" + appointment.date.day : appointment.date.day}
                    (${appointment.startTime.hour < 10 ? "0" + appointment.startTime.hour : appointment.startTime.hour}:${appointment.startTime.minute < 10 ? "0" + appointment.startTime.minute : appointment.startTime.minute})`}
                                    {id == appointment.hairDresserId ? (
                                        <PersonConverter id={appointment.guestId} />
                                    ) : (
                                        <></>
                                    )}
                                </div>
                            </Link>
                        </div>
                    ))}
                </div>
            ) : (
                <div className="no-appointments">
                    <p className="no-appointments-text">You don't have any appointments currently.</p>
                </div>
            )}
        </div>
    );
}

export default Appointments;