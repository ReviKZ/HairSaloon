import LoginForm from "../Form Components/LoginForm";

const IndexPage = () => {
    return (
        <>
            {localStorage.getItem("userId") == null ? <LoginForm /> :
                <div>
                    <h3>Logged in</h3>
                </div>
            }
        </>    
    );

};

export default IndexPage;