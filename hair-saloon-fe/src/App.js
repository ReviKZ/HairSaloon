import './App.css';
import { Routes, Route, Link } from 'react-router-dom';
import { Layout } from "./Components/Shared Components/Layout"
import { NotFound } from "./Components/Shared Components/NotFound"

function App() {
  return (
      <>
          {/* Main Router Tag */}
          <Routes>
              {/* Layout Tag */}
              <Route path="/" element={<Layout />} >
                  {/* Page Tags */}

                  {/* For Everyone */}
                  {/* Login Page */}
                  <Route path="" element={ } />

                  {/* Get Own User */}
                  <Route path="details" element={ } />

                  {/* Edit Own User */}
                  <Route path="edit" element={ } />

                  {/* Get Appointment List */}
                  <Route path="appointments" element={ } />

                  {/* Get Appointment */}
                  <Route path="appointments/:id" element={ } />


                  {/* HairDresser Routes */}
                  {/* Add Appointment */}
                  <Route path="appointments/create" element={ } />

                  {/* Edit Appointment */}
                  <Route path="appointments/edit/:id" element={ } />

                  {/* List Users */}
                  <Route path="users" element={ } />

                  {/* Register User */}
                  <Route path="register" element={ } />

                  {/* Get Other User */}
                  <Route path="details/:id" element={ } />

                  {/* Edit Other User */}
                  <Route path="edit/:id" element={ } />

              </Route>

              {/* Not-Found Page */}
              <Route path="*" element={<NotFound />} />

          </Routes>
      </>
  );
}

export default App;
