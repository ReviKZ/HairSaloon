import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import Fetch from "../Shared Components/Fetch";
import PersonConverter from "../Else/PersonConverter";

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
        <>
            <h1>Appointments</h1>
            {loading ?
                <p>Loading...</p>
                : appointments ?
                    appointments.map((appointment) => (
                        <div key={appointment.id}>
                            <Link to={"/appointments/" + appointment.id}>
                                <div>{appointment.date.year}.{appointment.date.month}.{appointment.date.day}
                                    ({appointment.startTime.hour}:{appointment.startTime.minute})
                                    {localStorage.getItem("userId") == appointment.hairDresserId ?
                                        (<PersonConverter id={appointment.guestId}/>) : <></>}</div>
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