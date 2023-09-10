import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch";

const Appointments = () => {
    const [loading, setLoading] = useState(true);
    const [appointments, setAppointments] = useState(false);
    const [guest, setGuest] = useState(false);

    useEffect(() => {
        const dataFetch = async () => {
            const appointmentData = await Fetch("get", "appointment/list/" + localStorage.getItem("userId"), "");
            const guestData = await Fetch("get", "user/" + appointmentData.guestId, "");
            setAppointments(appointmentData);
            setGuest(guestData);
            setLoading(false);
        }
        dataFetch();
    }, []);

    return (
        <>
            <h1>Appointments</h1>
            {loading ?
                <p>Loading...</p>
                : appointments ?
                    appointment.map((appointment) => (
                        <div key={appointment.id}>
                            <Link to={"/appointment/" + appointment.id}>
                                <div>{appointment.date.year}.{appointment.date.month}.{appointment.date.day}
                                    ({appointment.startTime.hour}:{appointment.startTime.minute})
                                    {localStorage.getItem("userId") == appointment.hairDresserId ?
                                        (<>- {guest.firstName} {guest.lastName}</>) : <></>}</div>
                            </Link>
                        </div>
                    )
                    ) :
                    (
                        <div>
                            <p>
                                You don't have any appointments currently.
                            </p>
                        </div>
                    )
            }
        </>
    );
}

export default Appointments;