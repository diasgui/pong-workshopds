defmodule PongBackendWeb.PlayerView do
  use PongBackendWeb, :view
  alias PongBackendWeb.PlayerView

  def render("index.json", %{players: players}) do
    %{data: render_many(players, PlayerView, "player.json")}
  end

  def render("show.json", %{player: player}) do
    %{data: render_one(player, PlayerView, "player.json")}
  end

  def render("player.json", %{player: player}) do
    %{
      name: player.name,
      wins: player.wins,
      losses: player.losses
    }
  end
end
