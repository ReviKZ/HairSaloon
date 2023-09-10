import { useEffect, useState, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch";

const Appointment = (id) => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [appointment, setAppointment] = useState(false);
    const [hairDresser, setHairDresser] = useState(false);
    const [guest, setGuest] = useState(false);

    useEffect(() => {
        const dataFetch = async () => {
            const appointmentData = await Fetch("get", "appointments/" + id, "");
            const hairDresserData = await Fetch("get", "user/" + appointmentData.hairDresserId, "");
            const guestData = await Fetch("get", "user/" + appointmentData.guestId, "");
            setAppointment(appointmentData);
            setHairDresser(hairDresserData);
            setGuest(guestData);
            setLoading(false);
        }
        dataFetch();
    }, []);

    const verifyAppointment = useCallback(() => {
        Fetch("patch", `appointment/${id}/verify`, "");
        navigate(`appointments/${id}`);
    }, []);

    return (
        <>
            <h1>Appointment Information</h1>
            {loading ?
                <p>Loading...</p>
                : appointment ?
                    (
                        <div>
                            <p>Date: {appointment.date.year}.{appointment.date.month}.{appointment.date.day}</p>
                            <p>Starting Time: {appointment.starTime.hour}:{appointment.startTime.minute}</p>
                            <p>Approx. Ending Time: {appointment.endTime.hour}:{appointment.endTime.minute}</p>
                            <p>Status of Appointmen {appointment.verified ? Verified : <>Not Verified</>}</p>
                            <p>Description of Appointment: {user.phoneNumber}</p>
                            {localStorage.getItem("userId") == hairDresser.id ? (<p>Your Guest : {guest.firstName} {guest.lastName}</p>) :
                                (<p>Your Hairdresser : {hairDresser.firstName} {hairDresser.lastName}</p>)}
                            <small>Is there a problem with the appointment? You want to get in contact, make corrections?</small>
                            <small>
                                You can reach your {localStorage.getItem("userId") == hairDresser.id ?
                                    (<small>Hairdresser at {hairDresser.phoneNumber} or write to {hairDresser.emailAddress}</small>)
                                    :
                                    (<small>Guest at {guest.phoneNumber} or write to {guest.emailAddress}</small>)
                                }
                            </small>
                            {localStorage.getItem("userId") == hairDresser.id ?
                                <></>
                                :
                                (
                                    <div>
                                        <small>Nothing wrong? Then verify the appointment!</small>
                                        <button type="button" onClick={verifyAppointment}>Verify</button>
                                    </div>
                                )
                            }
                        </div>
                    )
                    :
                    (
                        <div>
                            <h2>There isn't an Appointment with this id in the database.</h2>
                        </div>
                    )
            }
        </>
    );
}

export default Appointment;