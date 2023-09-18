import './App.css';
import { Routes, Route, Link } from 'react-router-dom';
import Layout from "./Components/Shared Components/Layout"
import NotFound from "./Components/Shared Components/NotFound";
import IndexPage from "./Components/Shared Components/IndexPage";
import User from "./Components/Single Components/User";
import Appointment from "./Components/Single Components/Appointment";
import Users from "./Components/Listing Components/Users";
import Appointments from "./Components/Listing Components/Appointments";
import RegisterForm from "./Components/Form Components/RegisterForm";
import EditUserForm from "./Components/Form Components/EditUserForm";
import RequireAuth from './Components/Else/RequireAuth';

function App() {
  return (
      <>
          {/* Main Router Tag */}
          <Routes>
              {/* Layout Tag */}
              <Route path="/" element={<Layout />} >
                  {/* Page Tags */}

                  {/* For Everyone */}
                  {/* Index Page */}
                  <Route path="" element={<IndexPage />} />

                  <Route element={<RequireAuth allowedRoles={[0, 1]} />}>
                      {/* Get Own User */}
                      <Route path="details" element={<User />} />

                      {/* Edit Own User */}
                      <Route path="edit" element={<EditUserForm />} />

                      {/* Get Appointment List */}
                      <Route path="appointments" element={<Appointments />} />

                      {/* Get Appointment */}
                      <Route path="appointments/:id" element={<Appointment />} />
                  </Route>


                  {/* HairDresser Routes */}
                  <Route element={<RequireAuth allowedRoles={[1]} />}>
                      {/* Add Appointment */}
                      <Route path="appointments/create" element={<IndexPage />} />

                      {/* Edit Appointment */}
                      <Route path="appointments/edit/:id" element={<IndexPage />} />

                      {/* List Users */}
                      <Route path="users" element={<Users />} />

                      {/* Register User */}
                      <Route path="register" element={<RegisterForm />} />

                      {/* Get Other User */}
                      <Route path="details/:personId" element={<User />} />

                      {/* Edit Other User */}
                      <Route path="edit/:personId" element={<EditUserForm />} />
                  </Route>

              </Route>

              {/* Not-Found Page */}
              <Route path="*" element={<NotFound />} />

          </Routes>
      </>
  );
}

export default App;
