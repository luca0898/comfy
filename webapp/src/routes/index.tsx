import React from 'react';
import { Switch, Route } from 'react-router-dom';
import DashboardScreen from '../pages/Dashboard/Dashboard';
import LoginScreen from '../pages/LoginScreen';

const Routes : React.FC = () => (
  <Switch>
    <Route path="/login" component={LoginScreen} exact />
    <Route path="/" component={DashboardScreen} exact />
  </Switch>
);

export default Routes;
