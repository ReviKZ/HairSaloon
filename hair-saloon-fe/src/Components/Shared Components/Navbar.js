import logo from "../../Pics/hslogo.png";
import { useEffect, useState, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { FaSignOutAlt } from "react-icons/fa";
import "../../Styling/Navbar.css";
import Fetch from "../Shared Components/Fetch";
import TokenConverter from "../Else/TokenConverter";


const Navbar = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [id, setId] = useState();
    const [user, setUser] = useState();

    useEffect(() => {
        const dataFetch = async () => {
            const idData = await TokenConverter();
            const userData = await Fetch("get", "user/" + idData, "");
            setId(idData);
            setUser(userData);
            setLoading(false);
        }
        dataFetch();
    }, []);

    const LogOut = useCallback(async () => {
        await localStorage.removeItem("userToken");
        await navigate(0);
    }, []);

    return (
        loading
            ?
            <></>
            :
            <nav className="navbar">
                <div className="navbar-container">
                    <div className="navbar-container-left">
                        <img src={logo} alt="HairSalon logo" className="hairsalon-logo" onClick={() => navigate("/")} />
                    </div>
                    <div className="navbar-container-right">
                        {user
                            ?
                            <>
                                <button className="navbar-button" onClick={() => navigate("/appointments")}>
                                    Appointments
                                </button>
                                {user.type === 1 ?
                                    <>
                                        <button className="navbar-button" onClick={() => navigate("/appointments/create")}>
                                            Create Appointment
                                        </button>

                                        <button className="navbar-button" onClick={() => navigate("/users")}>
                                            Search Users
                                        </button>

                                        <button className="navbar-button" onClick={() => navigate("/register")}>
                                            Register a User
                                        </button>
                                    </>
                                    :
                                    <></>
                                }
                                <button className="logout-button" type="button" onClick={LogOut}>
                                    <FaSignOutAlt />
                                </button>
                            </>
                            :
                            <></>
                            }
                    </div>
                </div>
            </nav>
        
    );
}

export default Navbar;