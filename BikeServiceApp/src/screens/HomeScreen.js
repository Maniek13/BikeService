import React, { Component } from 'react';
import {View, Text, Button} from 'react-native';
import LoginScreen from '../screens/LoginScreen';
import UsersTasksScreen from '../screens/UsersTasksScreen';

class HomeScreen extends Component {
  constructor(props){
    super(props);
  }

  
  
  render() {
    return (
      <View>
        <Button
          onPress={() => this.props.navigate('Login') }
          title="Log in"
        />
        <UsersTasksScreen/>
      </View>
    );
  }
}


export default HomeScreen;




