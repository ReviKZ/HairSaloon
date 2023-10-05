import { Helmet } from 'react-helmet';

const NotFound = () => {
    return (
        <>
            <Helmet><title>Not Found</title></Helmet>
            <h1>404: The Page you are looking for does not exist!</h1>
        </>
    );
}

export default NotFound;