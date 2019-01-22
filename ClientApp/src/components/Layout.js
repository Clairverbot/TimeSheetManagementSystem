import React, { Component } from 'react';
import { Container } from 'semantic-ui-react';
export class Layout extends Component {
  displayName = Layout.name

  render() {
    return (
          <div>
            <div 
            style={{ display: 'flex', justifyContent:'center', paddingLeft:210+'px', margin:2+'%'}}
            >
                {this.props.children}
            </div>
      </div>
    );
  }
}
