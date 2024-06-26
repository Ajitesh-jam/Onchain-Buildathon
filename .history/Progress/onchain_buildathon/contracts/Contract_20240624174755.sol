// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.9;

contract MyContract {
    mapping(address => uint256) public playerScores;
    struct player {
        address playerAddress;
        uint256 score;
        uint256[] skins;
    }
    player[] public players;

    event ScoreAdded(address indexed player, uint256 score);

    function isRegistered(address _player) private view returns (bool) {
        for (uint256 i = 0; i < players.length; i++) {
            if (address(uint160(players[i].playerAddress)) == _player) {
                return true;
            }
        }
        return false;
    }

    function submitScore(uint256 _score) external {
        require(_score > 0, "Score must be greater than 0");
        if (isRegistered(msg.sender)) {
            if (_score > playerScores[msg.sender]) {
                playerScores[msg.sender] = _score;
                emit ScoreAdded(msg.sender, _score);
            }
        } else {
            player memory newPlayer;
            newPlayer.playerAddress = msg.sender;
            newPlayer.score = _score;
            players.push(newPlayer);
            emit ScoreAdded(msg.sender, _score);
        }
    }

    function getRank(address _player) external view returns (uint256) {
        require(isRegistered(_player), "Player not registered");
        uint256 rank = 1;
        uint score = playerScores[_player];
        for (uint256 i = 0; i < players.length; i++) {
            if (score < playerScores[players[i].playerAddress]) {
                rank++;
            }
        }
        return rank;
    }

    function getSkins(address _player,unit256)
}
