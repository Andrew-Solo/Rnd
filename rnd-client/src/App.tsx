import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Root from './Root';

function App() {
  return (
    <Routes>
      <Route path="/" element={<Root/>}>
        <Route index />
      </Route>
    </Routes>
  );
}

export default App;
