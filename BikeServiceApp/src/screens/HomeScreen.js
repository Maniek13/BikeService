import React, { Component } from 'react';
import {View, Text, Button} from 'react-native';
import LoginScreen from '../screens/LoginScreen';
import UsersTasksScreen from '../screens/UsersTasksScreen';

class HomeScreen extends Component {
  constructor(props){
    super(props);

    this.state = {
      loginPage: false,
      userTask: true
    };
  }

  
  
  render() {
    return (
      <View>
        <Button
          onPress={() => {
            this.setState({ loginPage: true });
            this.setState({ userTask: false });
          }}
          title="Log in"
        />
        
        
        {this.state.loginPage === true ? <LoginScreen  navigate = {this.props.navigate}/> : ""}

        <UsersTasksScreen/>
      </View>
    );
  }
}


export default HomeScreen;




