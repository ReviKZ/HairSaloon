import Fetch from '../Shared Components/Fetch';

const TokenConverter = () => {
    const token = localStorage.getItem("userToken")

    const dataFetch = () => {
    return Fetch("post", "user/verify", token);
    }

    const data = dataFetch();
    
    return data
    

};

export default TokenConverter;