import React, { Component } from 'react';
import {Text, View, StyleSheet} from 'react-native';


class Error extends Component {
    constructor(props){
        super(props);
    }

  render() {
    return (
        <View style={styles.conteiner}>
            <Text style={{color: '#000000', fontSize: 23, textAlign: 'center'}} name='title'>Error</Text>
            <Text style={styles.text} name='state'>{this.props.error.code}</Text>
            <Text style={styles.text} name='state'>{this.props.error.message}</Text>
        </View>
    );
  }
}

export default Error;

const styles = StyleSheet.create({
  text : {
    color: '#000000',
    textAlign: 'center',
    marginTop: 10
  },
  button: {
    alignItems: 'center',
    marginLeft:'auto',
    marginRight:'auto',
    justifyContent: 'center',
    width: 100,
    padding: 5,
    backgroundColor: '#249ef0',
    borderRadius: 5,
    zIndex: 100
  },
  conteiner:{
    borderWidth: 1,
    borderRadius: 10,
    marginLeft: 'auto',
    marginRight: 'auto',
    width: 250,
    marginTop: 20,
    padding: 10,
    backgroundColor: 'white'
  }
});
