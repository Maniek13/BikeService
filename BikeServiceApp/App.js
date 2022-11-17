import React, { Component, useEffect } from 'react';
import { View, Text, Button} from 'react-native';
import PropTypes from 'prop-types';

import AdministrationListOfTasks from './src/screens/AdministrationListOfTasks';
import HomeScreen from './src/screens/HomeScreen';
import LoginScreen from './src/screens/LoginScreen';

import { NavigationContainer } from '@react-navigation/native'; 
import {createStackNavigator} from '@react-navigation/stack';

class Home extends React.Component {
  render() {
    const {navigate} = this.props.navigation;
      return (
        <HomeScreen navigation = {this.props.navigation}/>
      )
  }
}

class Admin extends React.Component {
  render() {
    return (
        <AdministrationListOfTasks navigate = {this.props.navigation.navigate}/>
    );
  }
}

class Login extends React.Component {
  render() {
    return (
        <LoginScreen navigate = {this.props.navigation.navigate}/>
    );
  }
}

const Stack  = createStackNavigator();
function Container() {
  return (
      <NavigationContainer>
          <Stack.Navigator initialRouteName="Home" screenOptions={{ gestureEnabled: true, headerLeft: null} } >
              <Stack.Screen
                name="Main"
                component={Home}
                options={{ 
                  title: 'Welcome to bike service app',
                  headerStyle: { backgroundColor: '#f4511e' },
                  headerTintColor: '#fff',
                  headerTitleAlign: 'center'                
                }}
              />
              <Stack.Screen
                name="Admin"
                component={Admin}      
              />
                <Stack.Screen
                name="Login"
                component={Login}      
              />
          </Stack.Navigator>
      </NavigationContainer>
  );
}
export default Container;  

Home.propTypes ={
  navigation: PropTypes.object.isRequired,
};
