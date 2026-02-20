import { useState } from "react";
import { getAllStudents } from "../api/studentApi";
import { callMicrosoft1 } from "../api/studentApi";

function StudentList() {
  const [students, setStudents] = useState([]);
  const [microsoftData, setMicrosoftData] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  // student btn call
  const loadStudents = async () => {
    setLoading(true);
    setError("");
    setStudents([]);

    try {
      const data = await getAllStudents();
      setStudents(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };
  // microsoft btn call
  const callMicrosoft = async () => {
    setLoading(true);
    setError("");
    setStudents([]);

    try {
      const data = await callMicrosoft1();
      setMicrosoftData(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <button onClick={loadStudents}>Get Students</button>
      <button onClick={callMicrosoft}>Call Microsoft</button>

      {loading && <p>Loading...</p>}
      {error && <p className="error">{error}</p>}

      {microsoftData && <p>{microsoftData}</p>}

      {students.length > 0 && (
        <table>
          <thead>
            <tr>
              <th>Id</th>
              <th>Student Name</th>
              <th>Email</th>
              <th>Address</th>
              <th>DOB</th>
            </tr>
          </thead>
          <tbody>
            {students.map((s) => (
              <tr key={s.id}>
                <td>{s.id}</td>
                <td>{s.studentName}</td>
                <td>{s.email}</td>
                <td>{s.address}</td>
                <td>{new Date(s.dob).toLocaleDateString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default StudentList;
