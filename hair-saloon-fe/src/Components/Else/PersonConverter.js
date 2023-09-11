import { useEffect, useState } from 'react';
import Fetch from "../Shared Components/Fetch"

const PersonConverter = ({ id }) => {
    const [userData, setUserData] = useState(false);

    useEffect(() => {
        const dataFetch = async () => {
            const data = await Fetch("get", "user/" + id, "");
            setUserData(data);
        }
        dataFetch();
    }, []);

    return (
        <>
            - {userData.firstName} {userData.lastName}
        </>
    );
};

export default PersonConverter;