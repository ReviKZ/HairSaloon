import Fetch from '../Shared Components/Fetch';

const TokenConverter = async () => {
    const tokenData = {
        token: localStorage.getItem("userToken")
    }

    const id = await Fetch("post", "user/verify", tokenData);

    return id;
};

export default TokenConverter;