import React, { Component } from 'react';
import { View, Text, Button} from 'react-native';

class LoginScreen extends Component {
  constructor(props){
    super(props);
  }

  render() {
    return (
      <View>
        <Text>Login</Text>
        <Button 
          onPress={() => this.props.navigate('Admin') }
          title="Log in"
        />
      </View>
    );
  }
}

export default LoginScreen;