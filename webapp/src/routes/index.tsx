import React from 'react';
import { Routes as Switch, Route } from 'react-router-dom';
import DashboardScreen from '../pages/Dashboard/Dashboard';
import LoginScreen from '../pages/LoginScreen';

const Routes : React.FC = () => (
  <Switch>
    <Route path="/login" element={<LoginScreen />} caseSensitive />
    <Route path="/" element={<DashboardScreen />} caseSensitive />
  </Switch>
);

export default Routes;
