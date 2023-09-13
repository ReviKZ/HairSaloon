import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch";
import PersonConverter from "../Else/PersonConverter";
import "../../Styling/Appointments.css";


const Appointments = () => {
    const [loading, setLoading] = useState(true);
    const [appointments, setAppointments] = useState(false);

    useEffect(() => {
        const dataFetch = async () => {
            const appointmentData = await Fetch("get", "appointment/list/" + localStorage.getItem("userId"), "");
            setAppointments(appointmentData);
            setLoading(false);
        }
        dataFetch();
    }, []);

    return (
        <div className="appointments-container">
            <h1 className="appointments-heading">Appointments</h1>
            {loading ? (
                <p className="loading-text">Loading...</p>
            ) : appointments ? (
                <div className="appointment-list">
                    {appointments.map((appointment) => (
                        <div key={appointment.id} className="appointment-item">
                            <Link to={`/appointments/${appointment.id}`} className="appointment-link">
                                <div className="appointment-details">
                                    {`${appointment.date.year}.${appointment.date.month}.${appointment.date.day} 
                    (${appointment.startTime.hour}:${appointment.startTime.minute})`}
                                    {localStorage.getItem("userId") == appointment.hairDresserId ? (
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